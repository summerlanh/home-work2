using Learning.ClassDatabase;


namespace Learning.Repository
{
    public class DatabaseAccessor
    {
        private static readonly LearningCenterEntities entities;

        static DatabaseAccessor()
        {
            entities = new LearningCenterEntities();
            entities.Database.Connection.Open();
        }

        public static LearningCenterEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}