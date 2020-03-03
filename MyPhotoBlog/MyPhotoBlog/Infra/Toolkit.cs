using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MyPhotoBlog.Infra
{

    public static class Toolkit
    {
        /// <summary>
        /// Bu metod yüklene fotoğrafı yeniden boyutlandırıp belirtilen klasöre yükler ve dosya yolunu döner
        /// </summary>
        /// <param name="image"></param>
        /// <param name="subfolder"></param>
        /// <param name="width"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ImageUpload(object image,string subfolder,int width,string format)
        {
            var resize = new ImageJob(image, "~/images/" + subfolder + "/<guid>", new Instructions("format=" + format + ";width=" + width + ";"))
            {
                CreateParentDirectory=true, AddFileExtension=true
            };

            resize.Build();

            return Path.GetFileName(resize.FinalPath);

        }

        
    }
}