import React from 'react';
import {Switch, Route} from 'react-router-dom';

import RegisterAndSignInPage from './pages/register-and-sign-in/register-and-sign-in.page.jsx';
import SignInPage from './pages/sign-in/sign-in.page.jsx';
import RegisterPage from './pages/register/register.page.jsx';
import GroupsPage from './pages/groups/groups.page.jsx';

import './App.scss';

class App extends React.Component {
  constructor(){
    super();
  }

  componentDidMount(){
    window.localStorage.setItem('reload', JSON.stringify(false));
  }

  render(){
    return (    
      <Switch>
        <Route exact path='/' component={RegisterAndSignInPage}></Route>
        <Route exact path='/signin' component={SignInPage} />
        <Route exact path='/register' component={RegisterPage} />
        <Route exact path='/groups' component={GroupsPage} />
      </Switch> 
    );
  }

}

export default App;