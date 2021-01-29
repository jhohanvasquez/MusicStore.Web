using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicStore.Data.DataAccessObjet.ModelAccess;
using MusicStore.Data.DataAccessObjet.ResourceAccess;

namespace MusicStore.Data.Business.BusinessLogic
{
    internal class ClientLogic
    {
        ClientDao clientDao = new ClientDao();
        public List<Client> ListClientorDetailLogic(string id)
        {
            return clientDao.ListClientorDetailDao(id);
        }

        public Client InsertorUpdateClientLogic(
                    string Id, string Name, string Mail, string Direction, string Phone, string Photo)
        {
            return clientDao.InsertorUpdateClientDao(Id, Name, Mail, Direction, Phone, Photo);
        }

        public void DeleteClientLogic(string id)
        {
            clientDao.DeleteClientDao(id);
        }
    }
}
