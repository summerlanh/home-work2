using System.Linq;
using Learning.Repository;

namespace Learning.Business
{
    public interface IClassManager
    {
        ClassModel[] Classes { get; }
        ClassModel TheClass(int classId);
    }

    public class ClassModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
    }

    public class ClassManager : IClassManager
    {
        private readonly IClassRepository classRepository;

        public ClassManager(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public ClassModel[] Classes
        {
            get
            {
                return classRepository.Classes
                                         .Select(t => new ClassModel { ClassId = t.ClassId, ClassName = t.ClassName, ClassDescription = t.ClassDescription, ClassPrice = t.ClassPrice })
                                         .ToArray();
            }
        }

        public ClassModel TheClass(int classId)
        {
            var classModel = classRepository.TheClass(classId);
            return new ClassModel { ClassId = classModel.ClassId, ClassName = classModel.ClassName, ClassDescription = classModel.ClassDescription, ClassPrice = classModel.ClassPrice };
        }
    }
}