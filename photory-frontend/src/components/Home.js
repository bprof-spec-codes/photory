import React, { useState } from "react";
import "./Home.css";
import Szívem from "../assets/Szívem.png";
import { TextField, Button  } from "@material-ui/core";
import { Link , useHistory } from "react-router-dom";
import axios from "../axios";
import ReactDOM from "react-dom";
import FacebookLogin from "react-facebook-login";
import {useDispatch , useSelector} from 'react-redux'
import {setActiveUser , selectValidationName ,selectToken,  selectPassword , selectRole} from '../features/userSlice'

function Home() {
  const [email, setEmail] = useState("Tomi");
  const [password, setPassword] = useState("bac4aac7-4d5e-48ba-9a31-e0352ba399dc");
  const history = useHistory();

  const dispatch = useDispatch();

  const loginHandler = () => {
    const data = {
      ValidationName: email,
      Password: password,
    };

    axios
      .put("/Auth/Login", data)
      .then((res) => {
        console.log(res.data);
        dispatch(setActiveUser({
          validationName:email,
          password: password,
          token:res.data.token,
          role:res.data.role,
          id : res.data.id
        }))
        console.log(res.data);
        history.push('/main');
        
      })
      .catch((err) => {
        console.log(err.message);
      });
  };

  const componentClicked = () =>{
      console.log();
  }


  

  const responseFacebook = (response) => {
    // console.log(response);
    // console.log(response.accessToken);
    const data = {
       accessToken: response.accessToken
    }
    axios.post("/ExternalAuth", data).then((res)=>{
      history.push('/main');
    }).catch((err)=>{
      console.log(err.message);
    });

  };

  return (
    <div className="home">
      <div className="home_left">
        <img loading="lazy" src={Szívem} />
      </div>
      <div className="home_right">
        <TextField
          label="Email/User Name"
          value={email}
          onChange={(e) => {
            setEmail(e.target.value);
          }}
        />     
         <TextField
          label="Password"
          value={password}
          onChange={(e) => {
            setPassword(e.target.value);
          }}
        />
        
        <Button onClick={loginHandler}>Login</Button>

        <p>
          If you dont have an account , please{" "}
          <Link to="/register" style={{ color: "black" }}>
            register
          </Link>
        </p>

        <FacebookLogin
          appId="287947399346523"
          autoLoad={false}
          fields="name,email,picture"
          onClick={componentClicked}
          callback={responseFacebook}
        />
      </div>
    </div>
  );
}

export default Home;
