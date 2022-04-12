namespace NetCoreMvcVueApp.Models
{
    public record User
    {
		public string UserName { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
	}
	public record IndexViewModel
	{
		public User User { get; set; }

		public List<User> FriendList { get; set; }
	}
}
