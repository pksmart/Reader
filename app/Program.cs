using System.Formats.Asn1;

namespace TeleprompterConsole;
internal class Program
{
    static async Task Main (string[] args)
    {
        // Read the file.
        var lines = ReadFrom("samplequotes.txt");
        foreach (var line in lines)
        {
            Console.WriteLine(line);
            if (string.IsNullOrWhiteSpace(line))
            {
                var pause = Task.Delay(200);
                // Synchronous waiting on atask ia an 
                // anti-pattern. Get fixed later
                pause.Wait();
            }
        }
    }
    static IEnumerable<string> ReadFrom(string file)
    {
        
        string? line;
        using (var reader = File.OpenText(file)) // The using initilaize the reader
        {
            while ((line = reader.ReadLine()) != null)
            {
                // Return sigle word
                var words = line.Split(' ');
                var lineLength = 0;
                foreach (var word in words)
                {
                    yield return word + " ";
                    lineLength += word.Length + 1;
                    if (lineLength > 70)
                    {
                        yield return Environment.NewLine;
                        lineLength = 0;
                    }

                }                
                yield return Environment.NewLine;
            }
        }

    }
    private static async Task  ShowTeleprompter()
    {
        var words = ReadFrom("samplequotes.txt");
        foreach (var word in words)
        {
            Console.WriteLine(word);
            if (!string.IsNullOrWhiteSpace(word))
            {
                var pause = Task.Delay(200);
                
                await Task.Delay(200);
                await ShowTeleprompter();
            }
        }
    } 
    private static async Task GetInput()
    {
        var delay = 200;
        Action work = () =>
        {
            do
            {
                var key = Console.ReadKey();
                if (key.KeyChar == '>')
                {
                    delay -= 10;
                }
                else if (key.KeyChar == '<')
                {
                    delay += 10;
                }
                else if(key.KeyChar =='X' || key.KeyChar == 'x')
                {
                    break;
                }

            } while (true);
        };
        await Task.Run(work);
    }      
}   
