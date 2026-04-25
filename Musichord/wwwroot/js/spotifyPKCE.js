'use strict';


export class SpotifyPKCE {
    generateRandomString(length) {
        const possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
        const values = crypto.getRandomValues(new Uint8Array(length));
        return values.reduce((acc, x) => acc + possible[x % possible.length], "");
    }

    async codeChallengeHash(plain) {
        const encoder = new TextEncoder()
        const data = encoder.encode(plain)
        return window.crypto.subtle.digest('SHA-256', data)
    }

    base64Encode(input) {
        return btoa(String.fromCharCode(...new Uint8Array(input)))
        .replace(/=/g, '')
        .replace(/\+/g, '-')
        .replace(/\//g, '_');
    }

    async codeChallenge() {
        const codeVerifier = this.generateRandomString(128);
        localStorage.setItem('codeVerifier', codeVerifier);
        const hash = await this.codeChallengeHash(codeVerifier);
        return this.base64Encode(hash);
    }
}