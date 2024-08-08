using SuperNumberProject.Common;
using SuperNumberProject.Models;
using static SuperNumberProject.Common.DataPoint;

namespace SuperNumberProject.Services
{
    public class SuperNumberServices
    {
        private readonly SuperNumberdbContext _context;

        public SuperNumberServices(SuperNumberdbContext context)
        {
            _context = context;
        }

        public ResultData CreateSuperNumber(int number, Guid iduser)
        {
            var existSN = _context.SuperNumbers.FirstOrDefault(sn => sn.Number == number);
            SuperNumber superNumber;

            if (existSN == null)
            {
                superNumber = new SuperNumber
                {
                    Id = Guid.NewGuid(),
                    Number = number,
                    SuperNumber1 = CalcSuperNumber(number)
                };
                _context.SuperNumbers.Add(superNumber);
                _context.SaveChanges();
            }
            else
            {
                superNumber = existSN;
            }

            var userSuperNumber = _context.UserSnumeros.FirstOrDefault(usn => usn.IdUser == iduser && usn.IdSuperNumber == superNumber.Id);

            if (userSuperNumber == null)
            {
                //por si ya existia el registro
                userSuperNumber = new UserSnumero
                {
                    Id = Guid.NewGuid(),
                    IdUser = iduser,
                    IdSuperNumber = superNumber.Id,
                    RegDate = DateTime.UtcNow,
                    IdSuperNumberNavigation = superNumber,
                    IdUserNavigation = _context.Users.Find(iduser)
                };
                _context.UserSnumeros.Add(userSuperNumber);
            }
            else
            {
                userSuperNumber.RegDate = DateTime.UtcNow;
                _context.UserSnumeros.Update(userSuperNumber);
            }
            _context.SaveChanges();

            return new ResultData { Result = "Ok", Data = superNumber.SuperNumber1.Value };
        }

        public ResultData DeleteHistoricalSuperNumbers(Guid iduser)
        {
            var userSuperNumbers = _context.UserSnumeros
                .Where(us => us.IdUser == iduser)
                .Select(us => us.IdSuperNumber)
                .Distinct()
                .ToList();

            if (userSuperNumbers.Any())
            {
                var userSuperNumberRecords = _context.UserSnumeros
                    .Where(us => us.IdUser == iduser)
                    .ToList();

                _context.UserSnumeros.RemoveRange(userSuperNumberRecords);


                _context.SaveChanges();
            }
            return new ResultData{Result ="Ok", Data = userSuperNumbers};
        }
        public List<HistoricalData> GetHistoricalData(Guid idUser)
        {

            var historicalData = _context.UserSnumeros
                .Where(x => x.IdUser == idUser)
                .Join(_context.SuperNumbers,
                      userSnumero => userSnumero.IdSuperNumber,
                      superNumero => superNumero.Id,
                      (userSnumero, superNumero) => new HistoricalData
                      {
                          Number = superNumero.Number.Value,
                          Result = superNumero.SuperNumber1.Value,
                          fecha = userSnumero.RegDate.Value
                      })
                .OrderByDescending(h => h.fecha) 
                .ToList();

            return historicalData;
        }

        public int CalcSuperNumber(int number) {
            int sumOfDigits = SumOfDigits(number);
            if (sumOfDigits < 10)
            {
                return sumOfDigits;
            }
            return CalcSuperNumber(sumOfDigits);
        }
        private int SumOfDigits(int number)
        {
            return number.ToString().Sum(c => c - '0');
        }
    }
}
