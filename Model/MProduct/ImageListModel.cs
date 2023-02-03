using ConstradeApi.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.Model.MProduct
{
    public class ImageListModel
    {
      
        public int ImageId { get; set; }

      
        public int ProductId { get; set; }

       
        public string ImageURL { get; set; }
    }
}
