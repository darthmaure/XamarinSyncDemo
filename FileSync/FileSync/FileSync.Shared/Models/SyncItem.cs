﻿using System;

namespace FileSync.Shared.Models
{
    public class SyncItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long Length { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
