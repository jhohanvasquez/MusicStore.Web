using MusicStore.Data.Business.BusinessLogic;
using MusicStore.Data.DataAccessObjet.ModelAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Data.Service.Facade
{
    public class ClientFacade
    {
        ClientLogic clientLogic = new ClientLogic();
        public List<Client> ListClientorDetailFacade(string id)
        {
            return clientLogic.ListClientorDetailLogic(id);
        }

        public Client InsertorUpdateClientFacade(
                    string Id, string Name, string Mail, string Direction, string Phone, string Photo)
        {
            return clientLogic.InsertorUpdateClientLogic(Id, Name, Mail, Direction, Phone, Photo);
        }

        public void DeleteClientFacade(string id)
        {
            clientLogic.DeleteClientLogic(id);
        }
    }
}
