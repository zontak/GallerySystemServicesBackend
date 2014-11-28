using GallerySystemServices.Data;
using GallerySysteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GallerySystemServices.Services.Managers
{
    public class CategoryManager
    {
        public GallerySystemServicesContext dbContext;

        public CategoryManager()
        {
            this.dbContext = WebApiApplication.dbContext;
        }

        public Category GetCategoryById(int id)
        {
            return dbContext.Categories.FirstOrDefault(c => c.Id == id);
        }
    }
}