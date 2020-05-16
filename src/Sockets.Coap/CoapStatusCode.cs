namespace Sockets.Coap
{
    public enum CoapStatusCode
    {
        // Calculation: (class << 5) | code
        // Success Codes
        None,
        Created = 65,
        Delete = 66,
        Valid = 67,
        Changed = 68,
        Content = 69,
        Continue = 95,

        // Client Error Codes
        BadRequest = 128,
        Unauthorized = 129,
        BadOption = 130,
        Forbidden = 131,
        NotFound = 132,
        MethodNotAllowed = 133,
        NotAcceptable = 134,
        RequestEntityIncomplete = 136,
        Conflict = 137,
        PreconditionFailed = 140,
        RequestEntityTooLarge = 141,
        UnsupportedContentFormat = 143,

        // Server Error Codes
        InternalServerError = 160,
        NotImplemented = 161,
        BadGateway = 162,
        ServiceUnavailable = 163,
        GatewayTimeout = 164,
        ProxyingNotSupported = 165,

        // Signaling Codes
        Unassigned = 224,
        Csm = 225,
        Ping = 226,
        Pong = 227,
        Release = 228,
        Abort = 229
    }
}
