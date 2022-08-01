using System.ComponentModel.DataAnnotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Features.AddItem;

public record AddItemEndpointRequestContract([Required] Guid BacklogId, [Required] string Name);
