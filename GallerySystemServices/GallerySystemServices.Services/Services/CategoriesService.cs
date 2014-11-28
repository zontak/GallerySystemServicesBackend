using GallerySystemServices.Services.Managers;
using GallerySysteServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GallerySystemServices.Services.Services
{
    public class CategoriesService
    {
        private CategoryManager categoryManager;

        public CategoriesService() 
        {
            this.categoryManager = new CategoryManager();
        }

        public Category GetCategoryById(int id)
        {
            return this.categoryManager.GetCategoryById(id);
        }

    }
}