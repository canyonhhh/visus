require 'rails_helper'

RSpec.describe "/employees", type: :request do

  let!(:business) { Business.create!(name: "Test Business", contact_info: "+37060000000") }

  let(:valid_attributes) {
    { name: 'John', email: 'test@example.com', role: Employee.roles.keys[0], business_id: business.id, password: 'password', password_confirmation: 'password' }
  }

  let(:invalid_attributes) {
    { name: nil, email: 'invalid_email', role: nil, business_id: nil, password: 'short', password_confirmation: 'mismatch' }
  }

  describe "GET /index" do
    it "renders a successful response" do
      Employee.create! valid_attributes
      get employees_url
      expect(response).to be_successful
    end
  end

  describe "GET /show" do
    it "renders a successful response" do
      employee = Employee.create! valid_attributes
      get employee_url(employee)
      expect(response).to be_successful
    end
  end

  describe "GET /new" do
    it "renders a successful response" do
      get new_employee_url
      expect(response).to be_successful
    end
  end

  describe "GET /edit" do
    it "renders a successful response" do
      employee = Employee.create! valid_attributes
      get edit_employee_url(employee)
      expect(response).to be_successful
    end
  end

  describe "POST /create" do
    context "with valid parameters" do
      it "creates a new Employee" do
        expect {
          post employees_url, params: { employee: valid_attributes }
        }.to change(Employee, :count).by(1)
      end

      it "redirects to the created employee" do
        post employees_url, params: { employee: valid_attributes }
        expect(response).to redirect_to(employee_url(Employee.last))
      end
    end

    context "with invalid parameters" do
      it "does not create a new Employee" do
        expect {
          post employees_url, params: { employee: invalid_attributes }
        }.to change(Employee, :count).by(0)
      end

      it "renders a response with 422 status (i.e. to display the 'new' template)" do
        post employees_url, params: { employee: invalid_attributes }
        expect(response).to have_http_status(:unprocessable_content)
      end
    end
  end

  describe "DELETE /destroy" do
    it "destroys the requested employee" do
      employee = Employee.create! valid_attributes
      expect {
        delete employee_url(employee)
      }.to change(Employee, :count).by(-1)
    end

    it "redirects to the employees list" do
      employee = Employee.create! valid_attributes
      delete employee_url(employee)
      expect(response).to redirect_to(employees_url)
    end
  end
end
