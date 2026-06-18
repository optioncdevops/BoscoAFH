namespace BoscoAFH.MasterInfrastructure
{
    public class SQLQuery
    {
        public class Category
        {
            public const string GetcategoryList = "SELECT category_id as CategoryId, category_name as CategoryName, created_by, created_on FROM public.categories";
        }
    }
}
