namespace Application.WorkItems.Requests
{
    public class CreateWorkItemRequest
    {
        public int ProjectId { get; set; }

        public string Title { get; set; } = string.Empty;
    }
}
