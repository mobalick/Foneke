namespace EMM.FoNeke.Common.WebUI
{
    public static class GenericFormBuilder
    {
        public static string GetFormFor<T>(T type)
        {
            return type.GetType().Name;
        }
    }
}