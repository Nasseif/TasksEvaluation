using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.Core.Entities.Business;

namespace TaskEvaluation.Core.Interfaces.IServices
{
    public interface IGroupService
    {
		//Task<IEnumerable<GroupDTO>> GetGroupsAsync();
		//Task<GroupDTO> GetGroupAsync(int id);
		Task<GroupDTO> GetGroupAsync(int id, params Expression<Func<Group, object>>[] includes);
		Task<IEnumerable<GroupDTO>> GetGroupsAsync(params Expression<Func<Group, object>>[] includes);
		Task<GroupDTO> CreateAsync(GroupDTO model);
		Task UpdateAsync(GroupDTO model);
		Task DeleteAsync(int id);
	}
}
