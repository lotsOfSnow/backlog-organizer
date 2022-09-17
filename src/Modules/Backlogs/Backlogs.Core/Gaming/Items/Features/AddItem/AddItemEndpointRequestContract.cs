using System.ComponentModel.DataAnnotations;

namespace BacklogOrganizer.Modules.Backlogs.Core.Gaming.Items.Features.AddItem;

public record AddItemEndpointRequestContract([Required] Guid BacklogId, [Required] string Name);
