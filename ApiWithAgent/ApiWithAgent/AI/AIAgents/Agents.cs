using ApiWithAgent.AI.AITools;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Agents.AI.Workflows;
using ApiWithAgent.AI.AIWorkflows;

namespace ApiWithAgent.AI.AIAgents
{
	public class Agents
	{
		private readonly AIAgent _writerAgent;
		private readonly AIAgent _editorAgent;
		private readonly AIAgent _workflowAgent;

		public Agents(IChatClient chatClient, WorkflowFactory workflowFactory)
		{
			_writerAgent = new ChatClientAgent(
				chatClient,
				instructions:
					"""
					You are a creative writing assistant who crafts vivid,
					well-structured stories with compelling characters based on user prompts,
					and formats them after writing.
					""",
				name: "Writer",
				description: "A creative writing assistant.",
				tools: [
					AIFunctionFactory.Create(Tools.GetAuthor),
					AIFunctionFactory.Create(Tools.FormatStory)
				]);

			_editorAgent = new ChatClientAgent(
				chatClient,
				instructions:
					"""
					You are an editor who improves a writer's draft by providing 4-8 concise recommendations and
					a fully revised Markdown document, focusing on clarity, coherence, accuracy, and alignment.
					""",
				name: "Editor",
				description: "Improves the writer draft.");

			_workflowAgent = workflowFactory.BuildSequentialWorkflowAgent(new[] { _writerAgent, _editorAgent });
		}

		public IEnumerable<AIAgent> GetAgents()
		{
			return [_writerAgent, _editorAgent];
		}

		public Task<AgentResponse> RunWorkflowAsync(string prompt)
		{
			return _workflowAgent.RunAsync(prompt);
		}
		
	}
}
