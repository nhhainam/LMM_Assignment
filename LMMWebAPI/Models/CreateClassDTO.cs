namespace LMM_WebClient.Models
{
	public class CreateClassDTO
	{

		public string ClassCode { get; set; } = null!;

		public string? Description { get; set; }
		public int CreatorId { get; set; }
	}
}
