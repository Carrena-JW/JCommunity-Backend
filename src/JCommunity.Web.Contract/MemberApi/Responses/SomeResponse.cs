﻿namespace JCommunity.Web.Contract.MemberApi.Responses;

public sealed record SomeResponse(int id, string name) : IResponseContract;
