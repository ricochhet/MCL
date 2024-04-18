using System;
using System.Collections.Generic;
using MCL.Core.MiniCommon.Models;

namespace MCL.Core.MiniCommon.Services;

public static class RequestDataService
{
    private static List<RequestData> Requests { get; set; } = [];
    public static int MaxSize { get; set; } = 100;

    public static void Add(RequestData item) => Requests.Add(item);

    public static void Clear() => Requests.Clear();

    public static void Init(Action<RequestData> func)
    {
        RequestData.OnRequestDataAdded += func;
        RequestData.OnRequestDataAdded += Manage;
    }

    private static void Manage(RequestData _)
    {
        if (Requests.Count > MaxSize)
            Requests.RemoveAt(0);
    }
}
