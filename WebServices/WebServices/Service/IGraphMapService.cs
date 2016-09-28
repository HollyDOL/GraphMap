using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WebServices.Service;

namespace WebServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGraphMapService" in both code and config file together.
    [ServiceContract]
    public interface IGraphMapService
    {

        [OperationContract]
        List<NodeDTO> GetAllData();

        [OperationContract]
        List<int> GetWayBetween(int source, int destination);

        // TODO: Add your service operations here
    }
}
