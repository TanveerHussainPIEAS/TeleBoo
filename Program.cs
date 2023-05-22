using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var botToken = "6269042327:AAHrzElyleLlbr20JIHTH6wcuM72bmQ-hkw"; // Replace with your actual bot token
            var groupId = new ChatId("@Alihassanil"); // Replace with your group's username or ID ("@Alihassanil");

            var botClient = new TelegramBotClient(botToken);

            Console.WriteLine("Press any key to stop the application.");

            while (!Console.KeyAvailable)
            {
                try
                {
                    var members = await botClient.GetChatAdministratorsAsync(groupId);
                    await SetMemberStatusToActiveAsync(botClient, groupId, members);
                    Console.WriteLine("Member statuses updated successfully.");
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the API calls
                    Console.WriteLine($"Failed to update member statuses: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1)); // Delay for 1 minute before the next iteration
            }
        }

        private static async Task SetMemberStatusToActiveAsync(TelegramBotClient botClient, ChatId groupId, ChatMember[] members)
        {
            foreach (var member in members)
            {
                if (member.User.IsBot)
                    continue;

                try
                {
                    await botClient.SendChatActionAsync(groupId, ChatAction.Typing);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that may occur during the API call
                    Console.WriteLine($"Failed to set status for member {member.User.Username}: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromSeconds(1)); // Delay between setting statuses (adjust as needed)
            }
        }
    }
}
