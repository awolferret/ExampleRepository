namespace SaveSystem
{
    public interface IDataPersistence
    {
        public void Save();
        public void Load();
        public DataToSave GetSaveSnapshot();
    }
}