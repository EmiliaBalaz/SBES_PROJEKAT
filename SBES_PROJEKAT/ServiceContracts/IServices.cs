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
        void AddPerson(string key, Osoba osoba,string nazivKlijenta);

        [OperationContract]
        void DeletePerson(string key,string nazivKlijenta);

        [OperationContract]
        void Read(string key, string nazivKlijenta);

        [OperationContract]
        void DeleteFile(string nazivKlijenta);
        [OperationContract]
        void CreateFile(string nazivKlijenta);

    }
}
