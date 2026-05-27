using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;

namespace ApiWithAgent.AI.AIWorkflows
{
    public class WorkflowFactory
    {
        public AIAgent BuildSequentialWorkflowAgent(IEnumerable<AIAgent> agents, string id = "my-workflow-agent", string name = "WorkflowAgent")
        {
            Workflow workflow = AgentWorkflowBuilder.BuildSequential(agents);
            return workflow.AsAIAgent(id: id, name: name);
        }
    }
}
