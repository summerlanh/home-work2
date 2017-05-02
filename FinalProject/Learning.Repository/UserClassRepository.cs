using System.Linq;

namespace Learning.Repository
{
    public class UserClassModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
    }

    public interface IUserClassRepository
    {
        UserClassModel Add(int userId, int classId);
        UserClassModel[] GetAll(int userId);
        bool Remove(int userId, int classId);
    }

    public class UserClassRepository : IUserClassRepository
    {
        public UserClassModel Add(int userId, int classId)
        {
            var classToAdd = DatabaseAccessor.Instance.Classes.Where(t => t.ClassId == classId).First();
            var user = DatabaseAccessor.Instance.Users.First(t => t.UserId == userId);

            user.Classes.Add(classToAdd);                                                
            DatabaseAccessor.Instance.SaveChanges();

            return new UserClassModel {};
        }

        public UserClassModel[] GetAll(int userId)
        {
            var user = DatabaseAccessor.Instance.Users.First(t => t.UserId == userId);
            var classes = user.Classes.Select(t => new UserClassModel { ClassId = t.ClassId, UserId = userId }).ToArray();

            return classes;
        }

        public bool Remove(int userId, int classId)
        {
            var user = DatabaseAccessor.Instance.Users.First(t => t.UserId == userId);
            var classToRemove = DatabaseAccessor.Instance.Classes.Where(t => t.ClassId == classId).First();

            user.Classes.Remove(classToRemove);
            DatabaseAccessor.Instance.SaveChanges();

            return true;
        }
    }
}
