using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.Serialization;
using GameLib.Models;

namespace GameLib
{
    public static class Extensions
    {
        public static byte[] sizeBytes(int size) => BitConverter.GetBytes(size);

        public static UserPlayer? getPlayer(Socket? client)
        {
            byte[] byteSize = new byte[4];
            client?.Receive(byteSize);

            byte[] dataPlayer = new byte[BitConverter.ToInt32(byteSize)];
            client?.Receive(dataPlayer);

            Utf8JsonReader utf8Reader = new Utf8JsonReader(dataPlayer);
            return JsonSerializer.Deserialize<UserPlayer>(ref utf8Reader);
        }

        public static GameField? getGameField(Socket? client)
        {
            byte[] byteSize = new byte[4];
            client?.Receive(byteSize);

            byte[] dataPlayer = new byte[BitConverter.ToInt32(byteSize)];
            client?.Receive(dataPlayer);

            Utf8JsonReader utf8Reader = new Utf8JsonReader(dataPlayer);
            return JsonSerializer.Deserialize<GameField>(ref utf8Reader);
        }

        public static void sentPlayer(Socket? client, UserPlayer? player)
        {
            byte[] buffer = JsonSerializer.SerializeToUtf8Bytes(player);
            client?.Send(sizeBytes(Buffer.ByteLength(buffer)));
            client?.Send(buffer);
        }

        public static void sentGameField(Socket? client, GameField? gameField)
        {
            byte[] buffer = JsonSerializer.SerializeToUtf8Bytes(gameField);
            client?.Send(sizeBytes(Buffer.ByteLength(buffer)));
            client?.Send(buffer);
        }
    }
}
