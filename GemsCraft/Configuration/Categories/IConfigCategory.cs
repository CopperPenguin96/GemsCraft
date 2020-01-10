namespace GemsCraft.Configuration.Categories
{
    public interface IConfigCategory
    {
        void SetKeyValue(string name, object value);

        object GetValue(string name);
    }
}
