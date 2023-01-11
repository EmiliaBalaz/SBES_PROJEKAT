using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    [ServiceContract]
    public interface IServices
    {
        [OperationContract]
        void AddPerson(string key, Osoba osoba);

        [OperationContract]
        bool DeletePerson(string key);

        [OperationContract]
        void Read(string key);

        [OperationContract]
        bool DeleteFile();
        [OperationContract]
        void CreateFile();

    }
}
