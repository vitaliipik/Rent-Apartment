 using System;
 using System.Collections.Generic;
 
 using System.Threading.Tasks;
 using reeltorapp.Models;
 using reeltorapp.ViewModels;
 using SQLite;
 using Xamarin.Essentials;
 using Xamarin.Forms;
 using HtmlAgilityPack;

 namespace reeltorapp.Services
 {
     public class InternetHouseService:IInternetHouseService
     {
       
         private static string BaseUrl = "https://www.booking.com/searchresults.en-gb.html?aid=304142&label=gen173nr-1DCBcoggI46AdIM1gEaOkBiAEBmAEJuAEXyAEV2AED6AEBiAIBqAIDuALel52VBsACAdICJDBiYTFhYTc3LTZmM2ItNDc0NS1hMzA2LTcwMzkxYTI4NmQ2M9gCBOACAQ&sid=1cfe77d91327b30db9a194c3375faba0&city=-1045268&";


         public void GetJobLinks()
         {
             HtmlWeb web = new HtmlWeb();
             HtmlDocument document = web.Load(BaseUrl);
             var trNodes = document.DocumentNode.SelectNodes("//div[@class='a826ba81c4 fe821aea6c fa2f36ad22 afd256fc79 d08f526e0d ed11e24d01 ef9845d4b3 da89aeb942']]");
             var htmlNode = document.DocumentNode.SelectNodes("//div[@class='fcab3ed991 a23c043802']]");
           
            
 
         }
         
        
       
     
     }
 }