/**
 * EasyHTTP Library
 * Library for making HTTP requests
 *
 * @version 1.0.0
 * @author  Andrea Bondanini
 *
 **/

export class HTTP {

    // http get
    async get(url) {
        const response = await fetch(url);
        const data = await response.json();
        return data;
    }

    // http post
    async post(url, data) {
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });
        const resData = await response.json();
        return resData;
    }
}