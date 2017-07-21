﻿using System.Net.Http;
using System.Web.Http.Tracing;
using System;

namespace HelloWebApi
{
    public class EntryExitTracer : ITraceWriter
    {
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level != TraceLevel.Off)
            {
                TraceRecord rec = new TraceRecord(request, category, level);
                traceAction(rec);
                RingBufferLog.Instance.Enqueue(rec);
            }
        }
    }
}