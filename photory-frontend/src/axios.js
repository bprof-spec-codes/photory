import axios from "axios";

const instance=axios.create({
    baseURL: 'https://projectphotory.azurewebsites.net' //API url
});

export default instance;