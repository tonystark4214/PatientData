using SmartdataPatient.Models;

namespace SmartdataPatient.Repository
{
    public interface IPatientData
    {
        public ResponseMessageModel GetAllPatientData();

        public ResponseMessageModel PostPatientData(PostModel model);

        public ResponseMessageModel DeletePatientData(int id,string userName);

        public ResponseMessageModel GetById(int id);
        public ResponseMessageModel Country();
    }
}
