import React from 'react';
import { withRouter } from 'react-router-dom';

import CustomForm from '../custom-form/custom-form.component.jsx';

class SignIn extends React.Component{
    constructor(props){
        super(props);

        this.state = {
            email_name: '',
            password: '',            
        };
        this.history = this.props.history;       
    }

    handleChange = e => {
        const { value, name } = e.target;
        this.setState({ [name]: value });
    }

    handleSubmit = e => {       
        e.preventDefault();
    }

    componentDidMount(){
     
    }

    render(){   
        
        const inputs = [
            {
                id: 'a10ff0c6-4113-4ad2-9a0a-4f5b5aca0595',
                name: 'email_name',
                type: 'text',
                label: 'email/username',
                value: this.state.email_name,
                required: true,
                onChange: this.handleChange
            },
            {
                id: '78b345b0-c227-4e65-a99f-9faef6bbc22f',
                name: 'password',
                type: 'password',
                label: 'password',
                value: this.state.password,
                required: true,
                onChange: this.handleChange
            }
        ];

        const buttons = [
            {
                id: '09d28880-1776-4f5a-8f63-b253ef73f5bf',
                type: 'submit',
                value: 'Submit Form',
                children: 'Sign in'
            }
        ];

        return(
            <div className='sign-in'>
               <h2>Sign in with your account</h2>
                <CustomForm inputs={inputs} buttons={buttons} onSubmition={this.handleSubmit} />
            </div>
        )
    }
}
export default withRouter(SignIn);