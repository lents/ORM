using DataAccess.LinqToDb.DataAccess;
using DataAccess.LinqToDb.Models;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.LinqToDb.Data
{
    public class StudentData
    {
        private readonly ApiDataContext _db;
        public StudentData(ApiDataContext db)
        {
            _db = db;
        }

        public Task<StudentModel?> GetStudentById(int id)
        {
            return _db.Students.FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
