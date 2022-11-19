using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using SkiaSharp;

namespace CatWorx.BadgeMaker
{

   class Util
   {
      //print to console
      public static void PrintEmployees(List<Employee> employees)
      {
         for (int i = 0; i < employees.Count; i++)
         {

            string template = "{0,-10}\t{1,-20}\t{2}";
            Console.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
         }
      }

      //populate csv file with
      public static void MakeCSV(List<Employee> employees)
      {
         if (!Directory.Exists("data"))
         {
            Directory.CreateDirectory("data");
         }
         using (StreamWriter file = new StreamWriter("data/employees.csv"))
         {
            // Any code that needs the StreamWriter would go in here

            string template = "{0,-10}\t{1,-20}\t{2}";
            file.WriteLine(String.Format(template, "ID", "Name", "PhotoUrl"));
            for (int i = 0; i < employees.Count; i++)
            {
               file.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
            }
         }
      }
      //create badge image

      async public static Task MakeBadges(List<Employee> employees)
      {
         //must declare pixel size to use bitmap
         int BADGE_WIDTH = 669;
         int BADGE_HEIGHT = 1044;

         //profile image margins
         int PHOTO_LEFT_X = 184;
         int PHOTO_TOP_Y = 215;
         int PHOTO_RIGHT_X = 486;
         int PHOTO_BOTTOM_Y = 517;

         int COMPANY_NAME_Y = 150;
         int EMPLOYEE_NAME_Y = 600;
         int EMPLOYEE_ID_Y = 730;

         using (HttpClient client = new HttpClient())
         {
            for (int i = 0; i < employees.Count; i++)
            {
               SKImage photo = SKImage.FromEncodedData(await client.GetStreamAsync(employees[i].GetPhotoUrl()));
               SKImage background = SKImage.FromEncodedData(File.OpenRead("badge.png"));

               // SKData data = background.Encode();
               // data.SaveTo(File.OpenWrite("data/employeeBadge.png"));
               SKBitmap badge = new SKBitmap(BADGE_WIDTH, BADGE_HEIGHT);
               //canvas allows us to edit the image
               SKCanvas canvas = new SKCanvas(badge);
               SKPaint paint = new SKPaint();
               paint.TextSize = 42.0f;
               paint.IsAntialias = true;
               paint.Color = SKColors.White;
               paint.IsStroke = false;
               paint.TextAlign = SKTextAlign.Center;
               paint.Typeface = SKTypeface.FromFamilyName("Arial");
               //allows us to allocate a position and size on the badge.

               // allows us to allocate a position and size on the badge.
               canvas.DrawImage(background, new SKRect(0, 0, BADGE_WIDTH, BADGE_HEIGHT));
               canvas.DrawImage(photo, new SKRect(PHOTO_LEFT_X, PHOTO_TOP_Y, PHOTO_RIGHT_X, PHOTO_BOTTOM_Y));
               // Company name


               // text
               // Company name
               canvas.DrawText(employees[i].GetCompanyName(), BADGE_WIDTH / 2f, COMPANY_NAME_Y, paint);

               // Employee name
               paint.Color = SKColors.Black;
               canvas.DrawText(employees[i].GetFullName(), BADGE_WIDTH / 2f, EMPLOYEE_NAME_Y, paint);
               // Employee Id
               paint.Typeface = SKTypeface.FromFamilyName("Courier New");
               canvas.DrawText(employees[i].GetId().ToString(), BADGE_WIDTH / 2f, EMPLOYEE_ID_Y, paint);


               SKImage finalImage = SKImage.FromBitmap(badge);
               SKData data = finalImage.Encode();
               data.SaveTo(File.OpenWrite($"data/{employees[i].GetId()}_badge.png"));
            }
         }

      }
   }
}