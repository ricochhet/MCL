using System;
using System.Collections.Generic;
using MCL.Core.MiniCommon.Models;

namespace MCL.Core.MiniCommon.Services;

public static class RequestDataService
{
    private static readonly List<RequestData> _requests = [];
    public static int MaxSize { get; set; } = 100;

    public static void Add(RequestData item) => _requests.Add(item);

    public static void Clear() => _requests.Clear();

    public static void Init(Action<RequestData> func)
    {
        RequestData.OnRequestDataAdded += func;
        RequestData.OnRequestDataAdded += Manage;
    }

    private static void Manage(RequestData _)
    {
        if (_requests.Count > MaxSize)
            _requests.RemoveAt(0);
    }
}
