using Api.Entities;

namespace Api.Interface
{
    public interface IData
    {
        public Task<List<Data>> GetDatas();
        public Data GetData(int id);
        public Task AddData(Data Data);
        public void UpdateData(Data Data);
        public Data DeleteData(int id);
        public bool CheckData(int id);
    }
}
