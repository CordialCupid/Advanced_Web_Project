'use strict'; 

export class SpotifyAJAXRepository {
    #AUTHORIZE = "https://accounts.spotify.com/authorize";
    #scopes = "user-read-private user-read-email user-library-modify playlist-modify-public playlist-modify-private playlist-read-private playlist-read-collaborative";
    #clientId = "ad61ff7774e443b99cf8123f95809301";
    #clientSecret = "a433dd0d69874f64b96d5a5cc193c496";
    #redirectURI = "http://127.0.0.1:5097/Identity/Account/Register";

    get authorize() {
        return this.#AUTHORIZE;
    }

    get secret() {
        return this.#clientSecret;
    }

    get scopes() {
        return this.#scopes;
    }

    get clientId() {
        return this.#clientId;
    }

    get redirectURI() {
        return this.#redirectURI;
    }
      
    requestAuth() {
        let url = this.authorize;
        url += "?client_id=" + this.clientId;
        url += "&response_type=code";
        url += "&redirect_uri=" + encodeURI(this.redirectURI);
        url += "&show_dialog=true";
        url += "&scope=" + this.scopes;
        window.location.href = url;
    }
}
