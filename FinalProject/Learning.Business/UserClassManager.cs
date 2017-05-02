using Learning.Repository;
using System.Linq;

namespace Learning.Business
{
    public interface IUserClassManager
    {
        UserClassModel Add(int userId, int classId);
        bool Remove(int userId, int classId);
        UserClassModel[] GetAll(int userId);
    }

    public class UserClassModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
    }

    public class UserClassManager : IUserClassManager
    {
        private readonly IUserClassRepository userClassRepository;
        private readonly IClassManager classManager;

        public UserClassManager(IUserClassRepository userClassRepository, IClassManager classManager)
        {
            this.userClassRepository = userClassRepository;
            this.classManager = classManager;
        }

        public UserClassModel Add(int userId, int classId)
        {
            var item = userClassRepository.Add(userId, classId);
            var className = classManager.TheClass(classId);

            return new UserClassModel { ClassId = item.ClassId, ClassName = className.ClassName, ClassDescription = className.ClassDescription, ClassPrice = className.ClassPrice};
        }

        public UserClassModel[] GetAll(int userId)
        {
            var items = userClassRepository.GetAll(userId)
                 .Select(t =>
                 {
                     var className = classManager.TheClass(t.ClassId);
                     return new UserClassModel { ClassId = t.ClassId, ClassName = className.ClassName, ClassDescription = className.ClassDescription, ClassPrice = className.ClassPrice };
                 })                   
                 .ToArray();
            return items;
        }

        public bool Remove(int userId, int classId)
        {
            return userClassRepository.Remove(userId, classId);
        }
    }
}