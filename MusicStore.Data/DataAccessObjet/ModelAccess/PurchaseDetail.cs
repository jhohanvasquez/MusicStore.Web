//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusicStore.Data.DataAccessObjet.ModelAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseDetail
    {
        public int Id { get; set; }
        public int Album_Id { get; set; }
        public string Client_Id { get; set; }
        public float Total { get; set; }
    
        public virtual AlbumSet AlbumSet { get; set; }
        public virtual Client Client { get; set; }
    }
}
