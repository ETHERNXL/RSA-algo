# RSA-algo
School project. Creating RSA cryptographic algorithm
As a result of the work, a program was obtained in which software encryption using the RSA method was implemented. 
Depending on the structure of the keys used, encryption methods are divided into:
symmetric encryption: third parties may know the encryption algorithm,
but a small portion of secret information - the key, which is the same for the sender and receiver of the message - is unknown; 
Examples: DES, 3DES, AES, Blowfish, Twofish, GOST 28147-89
asymmetric encryption: the encryption algorithm and possibly the public key may be known to outsiders, but the private key, 
known only to the recipient, is unknown. Public-key cryptographic systems are now widely used in various network protocols, 
in particular TLS and its predecessor SSL (which underlie HTTPS), as well as SSH, PGP, S/MIME, etc. uses asymmetric encryption - GOST R 34.10-2001.
At the moment, asymmetric encryption based on the public key RSA (decoded as Rivest, Shamir and Aldeman - the creators of the algorithm) is used by most 
products on the information security market.
Its crypto-resistance is based on the difficulty of factoring large numbers, namely, the exceptional difficulty of the task of determining 
the secret key based on the public key, since it is necessary to solve the problem of the existence of divisors of an integer. 
The most crypto-resistant systems use 1024-bit and large numbers.
