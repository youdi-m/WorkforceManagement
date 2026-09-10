const firstNames = ['John', 'Jane', 'Michael', 'Sarah', 'David', 'Emily', 'Chris', 'Amanda', 
                   'James', 'Patricia', 'Robert', 'Jennifer', 'William', 'Linda', 'Richard',
                   'Barbara', 'Joseph', 'Susan', 'Thomas', 'Jessica', 'Charles', 'Karen',
                   'Christopher', 'Lisa', 'Daniel', 'Nancy', 'Matthew', 'Betty', 'Anthony',
                   'Margaret', 'Mark', 'Sandra', 'Donald', 'Ashley', 'Steven', 'Kimberly',
                   'Paul', 'Donna', 'Andrew', 'Carol', 'Joshua', 'Michelle', 'Kenneth',
                   'Dorothy', 'Kevin', 'Melissa', 'Brian', 'Deborah', 'George', 'Stephanie'];

const lastNames = ['Smith', 'Johnson', 'Williams', 'Brown', 'Jones', 'Garcia', 'Miller', 'Davis',
                  'Rodriguez', 'Martinez', 'Hernandez', 'Lopez', 'Gonzalez', 'Wilson', 'Anderson',
                  'Thomas', 'Taylor', 'Moore', 'Jackson', 'Martin', 'Lee', 'Perez', 'Thompson',
                  'White', 'Harris', 'Sanchez', 'Clark', 'Ramirez', 'Lewis', 'Robinson',
                  'Walker', 'Young', 'Allen', 'King', 'Wright', 'Scott', 'Torres', 'Nguyen',
                  'Hill', 'Flores', 'Green', 'Adams', 'Nelson', 'Baker', 'Hall', 'Rivera'];

const titles = ['Software Engineer', 'Senior Developer', 'Product Manager', 'Data Analyst',
                'HR Specialist', 'Marketing Manager', 'Sales Representative', 'Accountant',
                'Customer Support', 'Operations Manager', 'Business Analyst', 'Team Lead',
                'Project Manager', 'UX Designer', 'QA Engineer', 'DevOps Engineer',
                'IT Support', 'Finance Manager', 'Recruiter', 'Content Writer'];

async function main() {
    // 1. Login
    console.log('Logging in...');
    const loginRes = await fetch('http://localhost:5016/api/auth/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: 'admin@company.com', password: 'Elpasso-00112' })
    });

    const loginData = await loginRes.json();
    const token = loginData.token || loginData.jwt || loginData.accessToken;

    if (!token) {
        console.log('Login failed:', loginData);
        return;
    }
    console.log('Logged in!');

    // 2. Create 100 employees
    for (let i = 0; i < 100; i++) {
        const firstName = firstNames[Math.floor(Math.random() * firstNames.length)];
        const lastName = lastNames[Math.floor(Math.random() * lastNames.length)];

        const employee = {
            firstName,
            lastName,
            title: titles[Math.floor(Math.random() * titles.length)],
            managerId: i === 0 ? null : Math.floor(Math.random() * 10) + 1,
            email: `${firstName.toLowerCase()}.${lastName.toLowerCase()}${Math.floor(Math.random() * 9999)}@company.com`,
            passwordHash: 'Password123!',
            status: 0,
            role: Math.floor(Math.random() * 3000),
            shiftStartTime: '09:00:00',
            shiftEndTime: '17:00:00',
            dateOfBirth: `${Math.floor(Math.random() * 30) + 1970}-${String(Math.floor(Math.random() * 12) + 1).padStart(2, '0')}-${String(Math.floor(Math.random() * 28) + 1).padStart(2, '0')}`
        };

        const res = await fetch('http://localhost:5016/api/employee', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(employee)
        });

        if (res.ok) {
            console.log(`✓ ${i + 1}/100: ${firstName} ${lastName}`);
        } else {
            const err = await res.text();
            console.log(`✗ ${i + 1}/100: ${firstName} ${lastName} - ${res.status}: ${err}`);
        }
    }
    console.log('Done!');
}

main();