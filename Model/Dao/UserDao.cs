using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        TestDBContext db = null;
        public UserDao()
        {
            db = new TestDBContext();
        }
        public IEnumerable<User> ListAll()
        {
            return db.Users;
        }
        public IEnumerable<User> ListAllBySort()
        {
            var userList = from s in db.Users select s; 
            userList = userList.OrderByDescending(s => s.GrID).ThenBy(s => s.ID);
            return userList;
        }
        public IEnumerable<User> ListAllByID()
        {
            var userList = from s in db.Users select s;
            userList = userList.OrderByDescending(s => s.ID);
            return userList;
        }
        public IEnumerable<User> ListAllGrID()
        {
            var userList = from s in db.Users select s;
            userList = userList.OrderBy(s => s.GrID);
            return userList;
        }
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public User GetById(int id)
        {
             return db.Users.Find(id);
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                user.GrID = entity.GrID;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
