﻿namespace JCommunity.Web.Contract.MemberApi.Requests;


public sealed record SomeRequest(int? id, string? name) : IRequestContract;
