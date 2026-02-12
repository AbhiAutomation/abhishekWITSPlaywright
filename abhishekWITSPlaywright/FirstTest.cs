using Microsoft.Playwright;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace abhishekWITSPlaywright
{
    internal class FirstTest
    {
       static async Task Main(string[] args) 
        {
            //Create an instance of playwright and launch a browser
            using var playwright =  await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

            //Create a new page and navigate to the desired URL 
             var page = await  browser.NewPageAsync();

            var url = "https://inline.app/booking/-Lamo24uNMzLIlnCEhIJ:inline-live-2a466/-Lamo28zt1ere32YxWMR?language=en";
            await page.GotoAsync(url);
        
            //(2) Click "Complete booking" button 

            await page.GetByRole(AriaRole.Button, new() {Name = "Complete booking" }).ClickAsync();
          

            // (3) Select 3 Adults in Party size dropdown list, 

            await page.SelectOptionAsync("#adult-picker", "3");
        
            // Dining date: Wed, Sat 20,

            await page.Locator("#date-picker").ClickAsync();
            await page.Locator("[data-date='2026-02-20']").ClickAsync();

            // Time: 19:30
        
            await page.GetByRole(AriaRole.Button, new() { Name = "19:30"}).ClickAsync();

                     //Create a function to  take Screenshot  and root folder 

            static string FindProjRoot()
            {
                // create instace of DirectoryInfo  
                var dir = new DirectoryInfo(AppContext.BaseDirectory);
                while (dir != null &&  !dir.GetFiles("*.csproj").Any())
                {
                     dir= dir.Parent;
                }

                             return  dir.FullName; 
            }


            ///NowUse this Absolute path which is  String 
            var projectPath = FindProjRoot();
            var ScreeshotPath= Path.Combine(projectPath, "screeshos");

            Console.WriteLine("ScreeshotPath :: => " + ScreeshotPath); 

            Directory.CreateDirectory(ScreeshotPath);

            // Create File name  and Full path for stroe  image

            var fileName = $"completeOrder_{DateTime.Now:yyyy_MM_dd_HH_mm}.png"; 
            var FullPath = Path.Combine(ScreeshotPath, fileName);
            Console.WriteLine("FullPath :: => " + FullPath);
            await page.ScreenshotAsync(new()
            {
                Path = FullPath, 
                FullPage = true 

            }); 

        }
    }
}
