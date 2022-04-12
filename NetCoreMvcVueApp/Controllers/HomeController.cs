using Microsoft.AspNetCore.Mvc;
using NetCoreMvcVueApp.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace NetCoreMvcVueApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
			var friendsList = new List<User>
			{
				new User
				{
					LastName = "Ford",
					Name = "Henry",
					UserName = "henryford121"
				},
				new User
				{
					LastName = "Montgo",
					Name = "Suzzie",
					UserName = "mongosus"
				},
				new User
				{
					LastName = "Rivera",
					Name = "Luis",
					UserName = "starraiden"
				}
			};

			TempData["TempDataFriendsList"] = JsonConvert.SerializeObject(friendsList);

			var model = new IndexViewModel
			{
				User = new User
				{
					LastName = "Rivera",
					Name = "Genesis",
					UserName = "genesisrrios"
				},
				FriendList = friendsList
			};

			return View(model);
        }

		[HttpPost]
		public bool InsertNewFriendInMemory([FromBody] User friend)
		{
			if (friend == default || !TempData.ContainsKey("TempDataFriendsList")) 
				return false;

			var tempData = TempData["TempDataFriendsList"];
			var deserializedData = JsonConvert.DeserializeObject<List<User>>((string)tempData);
			deserializedData.Add(friend);
			TempData["TempDataFriendsList"] = JsonConvert.SerializeObject(deserializedData);
			return true;
		}

		public List<User> GetFriendsList()
		{
			var tempData = TempData["TempDataFriendsList"];
			TempData.Keep();
			var deserializedData = JsonConvert.DeserializeObject<List<User>>((string)tempData);
			return deserializedData;
		}

		[HttpDelete]
		public bool RemoveFriend([FromBody] User friend)
		{
			if (friend == default || !TempData.ContainsKey("TempDataFriendsList")) 
				return false;

			var tempData = TempData["TempDataFriendsList"];
			var deserializedData = JsonConvert.DeserializeObject<List<User>>((string)tempData);
			deserializedData.Remove(friend);
			TempData["TempDataFriendsList"] = JsonConvert.SerializeObject(deserializedData);
			return true;
		}
		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}