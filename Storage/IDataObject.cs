﻿using System;

namespace Aleeda.Storage
{
    public interface IDataObject
    {
        bool INSERT(DatabaseClient dbClient);
        bool DELETE(DatabaseClient dbClient);
        bool UPDATE(DatabaseClient dbClient);
    }
}
