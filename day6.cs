public class Program
{
    private static readonly int[] dirs = [-1, 0, 1, 0, -1, 0];
    private static void Main(string[] args)
    {
        const string file_name = @"";

        int res_1 = 0, res_2 = 0;

        int n = 0, m = 0, current_dir = 0;
        HashSet<(int, int)> visited = new HashSet<(int, int)>(), obs = new HashSet<(int, int)>(), added_obs = new HashSet<(int, int)>();
        int[] guard_pos = [0, 0];

        foreach (string line in File.ReadAllLines(file_name))
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i].Equals('#'))
                {
                    obs.Add((n, i));
                }
                else if (line[i].Equals('^'))
                {
                    guard_pos[0] = n;
                    guard_pos[1] = i;
                }
            }
            n++;
            m = line.Length;
        }
        added_obs.Add((guard_pos[0], guard_pos[1]));

        while (true)
        {
            (int new_r, int new_c) = (guard_pos[0] + dirs[current_dir], guard_pos[1] + dirs[current_dir + 1]);
            if (new_r < 0 || new_r >= n || new_c < 0 || new_c >= m) break;
            if (obs.Contains((new_r, new_c)))
            {
                current_dir = (current_dir + 1) % 4;
            }
            else
            {
                if (visited.Add((guard_pos[0], guard_pos[1]))) res_1++;
                if (added_obs.Add((new_r, new_c)))
                {
                    obs.Add((new_r, new_c));
                    if (DoesLoop(obs, current_dir, guard_pos[0], guard_pos[1], n, m)) res_2++;
                    obs.Remove((new_r, new_c));
                }

                guard_pos[0] = new_r;
                guard_pos[1] = new_c;
            }
        }
        if (visited.Add((guard_pos[0], guard_pos[1]))) res_1++;
      
        Console.WriteLine($"Res 1 : {res_1}");
        Console.WriteLine($"Res 2 : {res_2}");
    }

    private static bool DoesLoop(HashSet<(int, int)> obs, int current_dir, int g_r, int g_c, int n, int m)
    {
        current_dir = (current_dir + 1) % 4;
        var visited = new HashSet<(int, int, int)>();
        while (true)
        {
            (int new_r, int new_c) = (g_r + dirs[current_dir], g_c + dirs[current_dir + 1]);
            if (new_r < 0 || new_r >= n || new_c < 0 || new_c >= m) break;
            if (obs.Contains((new_r, new_c)))
            {
                current_dir = (current_dir + 1) % 4;
            }
            else
            {
                if (!visited.Add((g_r, g_c, current_dir))) return true;
                g_r = new_r;
                g_c = new_c;
            }
        }

        return false;
    }
}
