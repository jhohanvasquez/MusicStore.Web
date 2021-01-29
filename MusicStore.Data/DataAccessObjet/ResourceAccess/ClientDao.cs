using MusicStore.Data.DataAccessObjet.ModelAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Data.DataAccessObjet.ResourceAccess
{
    internal class ClientDao
    {
        private BdMusicStore db = new BdMusicStore();
        public List<Client> ListClientorDetailDao(string id)
        {
            return db.ListorDetailClient(id).ToList();
        }

        public Client InsertorUpdateClientDao(
                    string Id, string Name, string Mail, string Direction, string Phone, string Photo)
        {
            return db.RegisterOrSaveUserClient(Id, Name, Mail, Direction, Phone, Photo).FirstOrDefault();
        }

        public void DeleteClientDao(string id)
        {
            db.DeleteClient(id);
        }
    }
}
